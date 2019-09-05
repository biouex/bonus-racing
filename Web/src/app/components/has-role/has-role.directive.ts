import { Directive, Input, ViewContainerRef, TemplateRef } from "@angular/core";
import { EnvironmentService } from "../../core/services/environment.service";

@Directive({
    selector: '[plrHasRole]'
})
export class HasRoleDirective {

    private userRoles: string[];

    constructor(
        private templateRef: TemplateRef<any>,
        private viewContainer: ViewContainerRef,
        environmentService: EnvironmentService
    ) {
        this.userRoles = environmentService.environment.roles;
    }

    @Input() set plrHasRole(roles: string[]) {
        let hasPermissions = this.checkRoles(roles);
        if (hasPermissions) {
            this.viewContainer.createEmbeddedView(this.templateRef);
        } else {
            this.viewContainer.clear();
        }
    }

    private checkRoles(roles: string[]): boolean {
        return roles.every(role => {
            return (this.userRoles.indexOf(role) >= 0);
        });
    }
}