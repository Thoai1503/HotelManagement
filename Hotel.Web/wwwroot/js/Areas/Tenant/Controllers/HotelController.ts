import $ from "jquery";
import BaseController from "../../../Controllers/BaseController";

class HotelController extends BaseController {
  public constructor(container: JQuery<HTMLElement>) {
    super(container);
  }

  protected override actions(): Record<string, () => void> {
    return {
      ...super.actions(),
      createOrUpdate: this.createOrUpdate,
    };
  }

  private createOrUpdate = (): void => {
    this.initializeOrganizationPicker();
  };

  private initializeOrganizationPicker = (): void => {
    const picker = this.container.find(".organization-picker");
    const select = picker.find<HTMLSelectElement>(".organization-select");
    const trigger = picker.find<HTMLButtonElement>(
      ".organization-picker-trigger",
    );
    const menu = picker.find(".organization-picker-menu");
    const current = picker.find(".organization-picker-current");
    if (
      !picker.length ||
      !select.length ||
      !trigger.length ||
      !menu.length ||
      !current.length
    )
      return;

    const options = menu.find<HTMLButtonElement>(".organization-picker-option");
    const placeholder = current.contents().clone();

    const closeMenu = (returnFocus = false): void => {
      menu.prop("hidden", true);
      trigger.attr("aria-expanded", "false");
      if (returnFocus) trigger.trigger("focus");
    };

    const updateCurrent = (option?: JQuery<HTMLButtonElement>): void => {
      current
        .empty()
        .append(option ? option.contents().clone() : placeholder.clone());

      options.each((_, item) => {
        $(item).attr("aria-selected", option?.is(item) ? "true" : "false");
      });
    };

    const selectOption = (option: JQuery<HTMLButtonElement>): void => {
      select.val(option.attr("data-value") ?? "").trigger("change");
      updateCurrent(option);
      closeMenu();
    };

    const syncSelection = (): void => {
      const selectedValue = select.val()?.toString() ?? "";
      const selectedOption = options
        .filter((_, option) => $(option).attr("data-value") === selectedValue)
        .first();
      updateCurrent(selectedOption.length ? selectedOption : undefined);
    };

    trigger.on("click", () => {
      const isOpening = Boolean(menu.prop("hidden"));
      menu.prop("hidden", !isOpening);
      trigger.attr("aria-expanded", String(isOpening));
      if (isOpening && options.length) options.first().trigger("focus");
    });

    options.each((index, item) => {
      console.log(`Initializing option: ${$(item).attr("data-value")}`);
      const option = $(item);
      option.on("click", () => selectOption(option));
      option.on("keydown", (event) => {
        let nextIndex: number | undefined;
        if (event.key === "ArrowDown") nextIndex = (index + 1) % options.length;
        else if (event.key === "ArrowUp")
          nextIndex = (index - 1 + options.length) % options.length;
        else if (event.key === "Home") nextIndex = 0;
        else if (event.key === "End") nextIndex = options.length - 1;
        else if (event.key === "Escape") {
          event.preventDefault();
          closeMenu(true);
          return;
        } else if (event.key === "Tab") {
          closeMenu();
          return;
        } else {
          return;
        }

        event.preventDefault();
        options.eq(nextIndex).trigger("focus");
      });
    });

    trigger.on("keydown", (event) => {
      if (
        (event.key === "ArrowDown" ||
          event.key === "Enter" ||
          event.key === " ") &&
        options.length
      ) {
        event.preventDefault();
        menu.prop("hidden", false);
        trigger.attr("aria-expanded", "true");
        const selectedValue = select.val()?.toString() ?? "";
        const selectedOption = options
          .filter((_, option) => $(option).attr("data-value") === selectedValue)
          .first();
        (selectedOption.length ? selectedOption : options.first()).trigger(
          "focus",
        );
      }
    });

    $(document)
      .off("click.hotelCreateOrUpdate")
      .on("click.hotelCreateOrUpdate", (event) => {
        if (!$(event.target).closest(picker).length) closeMenu();
      });

    select.on("change", syncSelection);
    syncSelection();
    select.prop("tabIndex", -1).attr("aria-hidden", "true");
    picker.addClass("is-enhanced");
  };
}

export default HotelController;
