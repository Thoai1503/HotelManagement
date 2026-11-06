import BaseController from "./BaseController";
import HotelController from "../Areas/Tenant/Controllers/HotelController";

type ControllerConstructor = new (
  container: JQuery<HTMLElement>,
) => BaseController;

class AppNavigator {
  private readonly controllerMappers: Record<string, ControllerConstructor> = {
    Hotel: HotelController,
  };

  public init(container: JQuery<HTMLElement>): void {
    const controllerName = container.attr("page-controller") ?? "";
    const Controller = this.controllerMappers[controllerName];
    if (!Controller) {
      console.error(`Controller [${controllerName}] is not registered.`);
      return;
    }

    new Controller(container).initializeAction();
  }
}

export default AppNavigator;
