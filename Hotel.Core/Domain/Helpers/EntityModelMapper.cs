using System.Globalization;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Hotel.Core.Domain.Helpers
{
    public static class EntityModelMapper
    {
        public static void CopyToEntity<TModel, TEntity>(
            TModel model,
            TEntity entity,
            IEntityType entityType,
            bool skipKeys = false)
        {
            ArgumentNullException.ThrowIfNull(model);
            ArgumentNullException.ThrowIfNull(entity);
            ArgumentNullException.ThrowIfNull(entityType);

            var keyNames = skipKeys
                ? entityType.FindPrimaryKey()?.Properties
                    .Select(property => property.Name)
                    .ToHashSet(StringComparer.OrdinalIgnoreCase)
                : null;

            foreach (var property in entityType.GetProperties())
            {
                if (property.PropertyInfo is not { CanWrite: true } entityProperty ||
                    (keyNames?.Contains(property.Name) ?? false))
                {
                    continue;
                }

                var modelProperty = FindProperty(typeof(TModel), property.Name);
                if (modelProperty is null || !modelProperty.CanRead)
                {
                    continue;
                }

                var value = modelProperty.GetValue(model);
                entityProperty.SetValue(
                    entity,
                    ConvertValue(value, entityProperty.PropertyType, property.Name));
            }
        }

        public static void CopyToModel<TEntity, TModel>(
            TEntity entity,
            TModel model,
            IEntityType entityType)
        {
            ArgumentNullException.ThrowIfNull(entity);
            ArgumentNullException.ThrowIfNull(model);
            ArgumentNullException.ThrowIfNull(entityType);

            foreach (var property in entityType.GetProperties())
            {
                if (property.PropertyInfo is not { CanRead: true } entityProperty)
                {
                    continue;
                }

                var modelProperty = FindProperty(typeof(TModel), property.Name);
                if (modelProperty is null || !modelProperty.CanWrite)
                {
                    continue;
                }

                modelProperty.SetValue(
                    model,
                    ConvertValue(
                        entityProperty.GetValue(entity),
                        modelProperty.PropertyType,
                        property.Name));
            }
        }

        public static object? GetModelValue<TModel>(TModel model, string propertyName)
        {
            ArgumentNullException.ThrowIfNull(model);
            ArgumentException.ThrowIfNullOrWhiteSpace(propertyName);

            var property = FindProperty(typeof(TModel), propertyName);
            if (property is null || !property.CanRead)
            {
                throw new InvalidOperationException(
                    $"Model type {typeof(TModel).Name} does not expose key property '{propertyName}'.");
            }

            return property.GetValue(model);
        }

        private static PropertyInfo? FindProperty(Type type, string propertyName)
        {
            return type.GetProperty(
                propertyName,
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
        }

        private static object? ConvertValue(object? value, Type destinationType, string propertyName)
        {
            if (value is null)
            {
                if (destinationType.IsValueType && Nullable.GetUnderlyingType(destinationType) is null)
                {
                    throw new InvalidOperationException(
                        $"Cannot map null to non-nullable property '{propertyName}'.");
                }

                return null;
            }

            var targetType = Nullable.GetUnderlyingType(destinationType) ?? destinationType;
            if (targetType.IsInstanceOfType(value))
            {
                return value;
            }

            if (targetType == typeof(Guid))
            {
                return Guid.Parse(Convert.ToString(value, CultureInfo.InvariantCulture)!);
            }

            if (targetType.IsEnum)
            {
                return value is string enumName
                    ? Enum.Parse(targetType, enumName, ignoreCase: true)
                    : Enum.ToObject(targetType, value);
            }

            return Convert.ChangeType(value, targetType, CultureInfo.InvariantCulture);
        }
    }
}
