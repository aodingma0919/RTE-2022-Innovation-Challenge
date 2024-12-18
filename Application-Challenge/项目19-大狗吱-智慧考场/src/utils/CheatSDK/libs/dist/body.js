"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};
exports.__esModule = true;
exports.createMsg = exports.getBrowserUUID = void 0;
var fingerprintjs_1 = require("@fingerprintjs/fingerprintjs");
var dayjs_1 = require("dayjs");
var advancedFormat_1 = require("dayjs/plugin/advancedFormat");
dayjs_1["default"].extend(advancedFormat_1["default"]);
/**
 * 获取当前浏览器唯一id
 * @returns
 */
function getBrowserUUID() {
    return __awaiter(this, void 0, Promise, function () {
        var fpPromise, fp, result, visitorId;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    fpPromise = fingerprintjs_1["default"].load();
                    return [4 /*yield*/, fpPromise];
                case 1:
                    fp = _a.sent();
                    return [4 /*yield*/, fp.get()];
                case 2:
                    result = _a.sent();
                    visitorId = result.visitorId;
                    console.log('============================');
                    console.log('');
                    console.log('浏览器唯一 id: ', visitorId);
                    console.log('');
                    console.log('============================');
                    return [2 /*return*/, visitorId];
            }
        });
    });
}
exports.getBrowserUUID = getBrowserUUID;
var __DEVICEID__;
exports.createMsg = function (eventName) { return __awaiter(void 0, void 0, Promise, function () {
    return __generator(this, function (_a) {
        switch (_a.label) {
            case 0:
                if (!!__DEVICEID__) return [3 /*break*/, 2];
                return [4 /*yield*/, getBrowserUUID()];
            case 1:
                __DEVICEID__ = _a.sent();
                _a.label = 2;
            case 2: return [2 /*return*/, {
                    deviceid: __DEVICEID__,
                    platform: 'web',
                    time: dayjs_1["default"]().format('X'),
                    eventname: eventName,
                    screenshots: [],
                    note: ''
                }];
        }
    });
}); };
