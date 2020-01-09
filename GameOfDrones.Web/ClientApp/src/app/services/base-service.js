"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var environment_1 = require("../../environments/environment");
var BaseService = /** @class */ (function () {
    function BaseService() {
        this.baseUrl = environment_1.environment.baseUrl;
        this.gameUrl = environment_1.environment.gameUrl;
        this.rankingUrl = environment_1.environment.rankingUrl;
        this.roundUrl = environment_1.environment.roundUrl;
    }
    return BaseService;
}());
exports.BaseService = BaseService;
//# sourceMappingURL=base-service.js.map