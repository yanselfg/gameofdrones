"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    }
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
Object.defineProperty(exports, "__esModule", { value: true });
var base_service_1 = require("./base-service");
var RankingService = /** @class */ (function (_super) {
    __extends(RankingService, _super);
    function RankingService(http) {
        var _this = _super.call(this) || this;
        _this.http = http;
        return _this;
    }
    RankingService.prototype.ranking = function () {
        return this.http.get(this.baseUrl + this.rankingUrl);
    };
    return RankingService;
}(base_service_1.BaseService));
exports.RankingService = RankingService;
//# sourceMappingURL=ranking-service.js.map