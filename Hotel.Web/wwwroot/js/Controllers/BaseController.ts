abstract class BaseController {
    public readonly controller: string;
    public readonly action: string;
    public readonly className: string;

    protected constructor(public readonly container: JQuery<HTMLElement>) {
        this.className = this.constructor.name;
        this.controller = container.attr("page-controller") ?? "";
        this.action = container.attr("page-action") ?? "";
    }

    protected actions(): Record<string, () => void> {
        return {};
    }

    public initializeAction(): void {
        const actionName = this.action.charAt(0).toLowerCase() + this.action.slice(1);
        const action = this.actions()[actionName];
        if (!action) {
            console.error(`Action [${this.action}] is not implemented by ${this.className}.`);
            return;
        }

        action();
    }
}

export default BaseController;
