# Hướng dẫn tổ chức JavaScript theo Controller/Action

JavaScript phía trình duyệt trong `Hotel.Web` được viết bằng TypeScript theo
mô hình OOP: mỗi controller là một class, mỗi action là một method. `App.ts`
khởi chạy `AppNavigator`; navigator tạo controller tương ứng với thuộc tính
`page-controller` trên view và gọi method khớp với `page-action`. Các truy vấn
và thao tác DOM sử dụng jQuery.

Không viết logic sự kiện trực tiếp trong Razor view và không sửa bundle đã
build.

## Cấu trúc thư mục

```text
wwwroot/js/
├── App.ts
├── Controllers/
│   ├── AppNavigator.ts
│   └── BaseController.ts
└── Areas/
    └── Tenant/
        └── Controllers/
            └── HotelController.ts
```

Tạo một file class cho mỗi controller trong area tương ứng. Các action của một
controller được tổ chức thành các method trong class đó, không tạo file riêng
cho từng action.

## Thêm action vào controller hiện có

Ví dụ thêm sự kiện cho action `Index` của Organization.

### 1. Thêm method vào controller

Trong `wwwroot/js/Areas/Tenant/Controllers/OrganizationController.ts`, kế thừa
`BaseController` và đăng ký method action:

```typescript
import $ from "jquery";
import BaseController from "../../../Controllers/BaseController";

class OrganizationController extends BaseController {
    public constructor(container: JQuery<HTMLElement>) {
        super(container);
    }

    protected override actions(): Record<string, () => void> {
        return {
            ...super.actions(),
            index: this.index
        };
    }

    private index = (): void => {
        const searchForm = this.container.find<HTMLFormElement>(".organization-search");
        searchForm.on("submit", event => {
            // Xử lý submit trong phạm vi trang Organization/Index.
        });
    };
}

export default OrganizationController;
```

Action trong C# có tên PascalCase; navigator chuyển ký tự đầu thành chữ thường
khi tìm method TypeScript, ví dụ `Index` thành `index`,
`CreateOrUpdate` thành `createOrUpdate`. Đăng ký method trong `actions()` bằng
khóa tương ứng.

Dùng `this.container.find(...)` để giới hạn truy vấn trong trang hiện tại. Với
sự kiện trên `document` hoặc phần tử dùng chung, đặt namespace và gỡ handler
cùng namespace trước khi đăng ký lại, ví dụ
`$(document).off("click.organizationIndex").on("click.organizationIndex", ...)`.

### 2. Đăng ký controller trong navigator

Trong `wwwroot/js/Controllers/AppNavigator.ts`, import class và thêm vào
`controllerMappers`:

```typescript
import OrganizationController from "../Areas/Tenant/Controllers/OrganizationController";

private readonly controllerMappers: Record<string, ControllerConstructor> = {
    Hotel: HotelController,
    Organization: OrganizationController
};
```

Tên khóa phải khớp chính xác với giá trị `page-controller`. Không cần thêm
import action riêng trong `App.ts`; navigator chịu trách nhiệm phân phối tới
controller/action.

### 3. Đánh dấu view

Đặt hai thuộc tính lên phần tử bao ngoài nội dung action. Giá trị khớp với tên
controller và action trong MVC:

```cshtml
<div page-controller="Organization" page-action="Index">
    <form class="organization-search">
        ...
    </form>
</div>
```

### 4. Nạp bundle trong view

View có sự kiện cần nạp bundle qua `Scripts` section:

```cshtml
@section Scripts {
    <environment include="Development">
        <script src="~/js/tenant.bundle.js" asp-append-version="true"></script>
    </environment>
    <environment exclude="Development">
        <script src="~/js/tenant.bundle.min.js" asp-append-version="true"></script>
    </environment>
}
```

Layout chung render section này ở cuối trang. Chỉ thêm section một lần cho view
cần JavaScript. jQuery đã được đóng gói cùng bundle; không thêm CDN hoặc nạp
jQuery lần nữa trong view.

## Thêm controller mới

1. Tạo `wwwroot/js/Areas/<Area>/Controllers/<Controller>Controller.ts`.
2. Kế thừa `BaseController`, thêm method cho action và đăng ký chúng trong
   `actions()`.
3. Import class và thêm vào `controllerMappers` trong `AppNavigator.ts`.
4. Thêm `page-controller` và `page-action` vào view.
5. Nạp bundle trong `Scripts` section của view.

## Build và cập nhật bundle

Chạy lệnh từ thư mục `Hotel.Web`:

```powershell
npm ci
npm run typecheck
npm run build
```

`npm ci` dùng khi cần cài dependency theo đúng `package-lock.json`.
`npm run build` tự type-check rồi tạo/cập nhật:

- `wwwroot/js/tenant.bundle.js` cho Development.
- `wwwroot/js/tenant.bundle.min.js` cho môi trường khác.

Build lại sau mỗi thay đổi TypeScript và trước khi kiểm tra hoặc publish ứng
dụng. Chỉ sửa file `.ts`, không sửa trực tiếp bundle.

## Kiểm tra trước khi hoàn tất

- Tên `page-controller` có trong `controllerMappers`.
- Tên `page-action` khớp một method đã đăng ký trong `actions()`.
- View nạp bundle đúng một lần qua `Scripts` section.
- Truy vấn jQuery được giới hạn trong `this.container` khi phù hợp.
- `npm run build` hoàn tất và thao tác đã được kiểm tra trên trang.
