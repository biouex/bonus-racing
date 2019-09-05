import { AppInitService } from "./app-Init.service";

export function init(initService: AppInitService) {
    return () => initService.prepareForBootstrap();
}