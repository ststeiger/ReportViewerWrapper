var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var Http1;
(function (Http1) {
    var RequestBase = (function () {
        function RequestBase() {
            this.complete = false;
            this.m_SuccessCallbacks = [];
            this.m_CancelCallbacks = [];
            this.m_NetworkFailureCallbacks = [];
            this.m_FailureCallbacks = [];
            this.m_CompleteCallbacks = [];
            this.m_ProgressCallbacks = [];
            this.m_ResultHook = [];
        }
        RequestBase.prototype.successCallback = function () {
            this.complete = true;
            for (var i = 0; i < this.m_SuccessCallbacks.length; ++i) {
                this.m_SuccessCallbacks[i].apply(this, arguments);
            }
        };
        RequestBase.failureDefault = function (r) {
            console.log("failure");
            console.log(r);
            var msg = "Error " + r.status + " (" + r.statusText + "): \n\n";
            msg += "URL: \n" + r.responseURL + "\n\n";
            msg += r.responseText;
            alert(msg);
        };
        RequestBase.prototype.failureCallback = function () {
            var args = [];
            for (var _i = 0; _i < arguments.length; _i++) {
                args[_i] = arguments[_i];
            }
            this.complete = true;
            if (this.m_FailureCallbacks.length === 0)
                RequestBase.failureDefault.apply(this, arguments);
            for (var i = 0; i < this.m_FailureCallbacks.length; ++i) {
                this.m_FailureCallbacks[i].apply(this, arguments);
            }
        };
        RequestBase.prototype.cancelCallback = function () {
            this.complete = true;
            for (var i = 0; i < this.m_CancelCallbacks.length; ++i) {
                this.m_CancelCallbacks[i].apply(this, arguments);
            }
        };
        RequestBase.prototype.networkFailureCallback = function () {
            var args = [];
            for (var _i = 0; _i < arguments.length; _i++) {
                args[_i] = arguments[_i];
            }
            this.complete = true;
            if (this.m_NetworkFailureCallbacks.length === 0)
                this.failureCallback.apply(this, arguments);
            for (var i = 0; i < this.m_NetworkFailureCallbacks.length; ++i) {
                this.m_NetworkFailureCallbacks[i].apply(this, arguments);
            }
        };
        RequestBase.prototype.alwaysCallback = function () {
            var args = [];
            for (var _i = 0; _i < arguments.length; _i++) {
                args[_i] = arguments[_i];
            }
            this.complete = true;
            for (var i = 0; i < this.m_CompleteCallbacks.length; ++i) {
                this.m_CompleteCallbacks[i].apply(this, arguments);
            }
        };
        RequestBase.prototype.resultHookCallback = function (a) {
            for (var i = 0; i < this.m_ResultHook.length; ++i) {
                a = this.m_ResultHook[i].apply(this, arguments);
            }
            return a;
        };
        RequestBase.prototype.progressCallback = function () {
            this.complete = true;
            for (var i = 0; i < this.m_ProgressCallbacks.length; ++i) {
                this.m_ProgressCallbacks[i].apply(this, arguments);
            }
        };
        RequestBase.prototype.success = function (cb) {
            if (cb != null)
                this.m_SuccessCallbacks.push(cb);
            else
                Error("Success-callback is NULL or UNDEFINED.");
            return this;
        };
        RequestBase.prototype.cancel = function (cb) {
            if (cb != null)
                this.m_CancelCallbacks.push(cb);
            else
                Error("Cancel-callback is NULL or UNDEFINED.");
            return this;
        };
        RequestBase.prototype.networkFailure = function (cb) {
            if (cb != null)
                this.m_NetworkFailureCallbacks.push(cb);
            else
                Error("Network-failure-callback is NULL or UNDEFINED.");
            return this;
        };
        RequestBase.prototype.failure = function (cb) {
            if (cb != null)
                this.m_FailureCallbacks.push(cb);
            else
                Error("Failure-callback is NULL or UNDEFINED.");
            return this;
        };
        RequestBase.prototype.always = function (cb) {
            if (cb != null)
                this.m_CompleteCallbacks.push(cb);
            else
                Error("Always-callback is NULL or UNDEFINED.");
            return this;
        };
        RequestBase.prototype.progress = function (cb) {
            if (cb != null)
                this.m_ProgressCallbacks.push(cb);
            else
                Error("Failure-callback is NULL or UNDEFINED.");
            return this;
        };
        RequestBase.prototype.resultHook = function (fn) {
            if (fn != null)
                this.m_ResultHook.push(fn);
            else
                Error("Result-hook-callback is NULL or UNDEFINED.");
            return this;
        };
        RequestBase.prototype.send = function (data) {
            var url = this.url, postData = null;
            if (this.method == null)
                this.method = "GET";
            if (this.contentType == null)
                this.contentType = 'application/urlencode';
            if (this.cache == null || this.cache == false) {
                url += ((this.url.indexOf('?') === -1) ? "?" : "&") + "no_cache=" + (new Date()).getTime();
            }
            if (this.queryData != null) {
                if (!(typeof this.queryData == 'string' || this.queryData instanceof String)) {
                    var query = [];
                    for (var i = 0, keys = Object.keys(this.queryData); i < keys.length; i++) {
                        query.push(encodeURIComponent(keys[i]) + '=' + encodeURIComponent(this.queryData[keys[i]]));
                    }
                    url += ((this.url.indexOf('?') === -1) ? "?" : "&") + query.join('&');
                }
                else {
                    var qs = this.queryData;
                    if (qs.substr(0, 1) === "?" || qs.substr(0, 1) === "&") {
                        qs = qs.substr(1);
                    }
                    url += ((this.url.indexOf('?') === -1) ? "?" : "&") + qs;
                }
            }
            if (this.postData != null) {
                if (this.method != null && this.method.toLowerCase() !== "post") {
                    Error("Can't have postData when method is not POST.");
                }
                if (this.method == null) {
                    this.method = "POST";
                }
                if (this.contentType == null)
                    this.contentType = 'application/x-www-form-urlencoded';
                if (this.contentType.indexOf("application/json") != -1) {
                    if (!(typeof this.postData == 'string' || this.postData instanceof String))
                        postData = JSON.stringify(postData);
                    else
                        postData = this.postData;
                }
                else {
                    if (this.postData instanceof FormData) {
                        this.contentType = null;
                        postData = this.postData;
                    }
                    else if (!(typeof this.postData == 'string' || this.postData instanceof String)) {
                        var query = [];
                        for (var i = 0, keys = Object.keys(this.postData); i < keys.length; i++) {
                            query.push(encodeURIComponent(keys[i]) + '=' + encodeURIComponent(this.postData[keys[i]]));
                        }
                        postData = query.join('&');
                    }
                    else
                        postData = this.postData;
                }
            }
            var req = new XMLHttpRequest();
            if (req.upload && req.upload.addEventListener) {
                req.upload.addEventListener('progress', this.progressCallback, false);
            }
            if (req.onprogress)
                req.onprogress = this.progressCallback.bind(this);
            if (req == null) {
                this.failureCallback(req, "Browser doesn't support XmlHttpRequest...");
                return;
            }
            var that = this;
            req.onerror = function (ev) {
                console.log("req.onerror:", this, ev);
                that.networkFailureCallback(req, "There was an unexpected network error.\nSee the console log.\nError Details:\n" + ev);
                that.alwaysCallback(req, "There was an unexpected network error.\nSee the console log.\nError Details:\n" + ev);
                return false;
            }.bind(this);
            if (this.user != null && this.password != null)
                req.open(this.method, url, true, this.user, this.password);
            else
                req.open(this.method, url, true);
            if (this.cache !== true)
                req.setRequestHeader('cache-control', 'no-cache');
            if (this.contentType != null)
                req.setRequestHeader('Content-type', this.contentType);
            req.onreadystatechange = function () {
                if (req.readyState != 4)
                    return;
                if (!(req.status != 200 && req.status != 304 && req.status != 0)) {
                    if (this.contentType != null && this.contentType.toLowerCase().indexOf("application/json") !== -1) {
                        var obj = null, jsonParseSuccessful = false;
                        try {
                            obj = JSON.parse(req.responseText);
                            jsonParseSuccessful = true;
                        }
                        catch (e) {
                            console.log(e.name);
                            console.log(e.message);
                            console.log(e.stack);
                            console.log(e);
                            console.log(req);
                            console.log(req.responseText);
                            this.failureCallback(req);
                        }
                        if (jsonParseSuccessful) {
                            if (obj.error == null) {
                                this.successCallback(obj.data);
                            }
                            else {
                                this.failureCallback(req, obj.error);
                            }
                        }
                    }
                    else {
                        var response = req.responseText;
                        var noResponseProcessingError = false;
                        try {
                            response = this.resultHookCallback(response);
                            noResponseProcessingError = true;
                        }
                        catch (e) {
                            console.log('Result pre-processing error:\r\n', e);
                            this.failureCallback(req);
                        }
                        if (noResponseProcessingError)
                            this.successCallback(response);
                    }
                }
                if (req.status != 200 && req.status != 304 && req.status != 0) {
                    this.failureCallback(req);
                    console.log("aaa");
                }
                if (req.status === 304 || req.status === 0) {
                    this.cancelCallback(req);
                }
                this.alwaysCallback(req);
            }.bind(this);
            if (req.readyState == 4)
                return;
            req.send(postData);
            return this;
        };
        RequestBase.prototype.sendAsync = function () {
            return new Promise(function (resolve, reject) {
                this.success(function (result) {
                    resolve(result);
                });
                this.failure(function (xhr) {
                    console.log("onError", arguments);
                    reject(arguments);
                });
                this.cancel(function (xhr) {
                    console.log("onCancel", arguments);
                    reject(arguments);
                });
                this.send();
            }.bind(this));
        };
        return RequestBase;
    }());
    Http1.RequestBase = RequestBase;
    var Get = (function (_super) {
        __extends(Get, _super);
        function Get(url, data, success) {
            var _this = _super.call(this) || this;
            if (success != null)
                _this.success(success);
            _this.method = "GET";
            _this.url = url;
            _this.queryData = data;
            return _this;
        }
        return Get;
    }(RequestBase));
    Http1.Get = Get;
    var Post = (function (_super) {
        __extends(Post, _super);
        function Post(url, data, success) {
            var _this = _super.call(this) || this;
            if (success != null)
                _this.success(success);
            _this.method = "POST";
            _this.url = url;
            _this.contentType = "application/x-www-form-urlencoded";
            _this.postData = data;
            return _this;
        }
        return Post;
    }(RequestBase));
    Http1.Post = Post;
    var PostEval = (function (_super) {
        __extends(PostEval, _super);
        function PostEval(url, data, success) {
            var _this = _super.call(this) || this;
            _this.method = "POST";
            _this.url = url;
            _this.contentType = "application/x-www-form-urlencoded";
            _this.postData = data;
            if (success != null)
                _this.success(success);
            _this.resultHook(function (data) {
                return eval('(' + data + ')');
            }.bind(_this));
            return _this;
        }
        return PostEval;
    }(RequestBase));
    Http1.PostEval = PostEval;
    var PostJSON = (function (_super) {
        __extends(PostJSON, _super);
        function PostJSON(url, data, success) {
            var _this = _super.call(this) || this;
            _this.method = "POST";
            _this.contentType = "application/x-www-form-urlencoded";
            _this.url = url;
            _this.postData = data;
            if (success != null)
                _this.success(success);
            _this.resultHook(function (data) {
                return JSON.parse(data);
            }.bind(_this));
            return _this;
        }
        return PostJSON;
    }(RequestBase));
    Http1.PostJSON = PostJSON;
    var Json = (function (_super) {
        __extends(Json, _super);
        function Json(url, data, success) {
            var _this = _super.call(this) || this;
            if (success != null)
                _this.success(success);
            _this.method = "POST";
            _this.contentType = "application/json; charset=UTF-8";
            _this.url = url;
            _this.postData = data;
            if (success != null)
                _this.success(success);
            _this.resultHook(function (data) {
                return JSON.parse(data);
            }.bind(_this));
            return _this;
        }
        return Json;
    }(RequestBase));
    Http1.Json = Json;
    var JSONP = (function (_super) {
        __extends(JSONP, _super);
        function JSONP(url, data, success) {
            var _this = _super.call(this) || this;
            _this.send.bind(_this);
            _this.sendAsync.bind(_this);
            _this.url = url;
            _this.queryData = data;
            if (success != null)
                _this.success(success);
            return _this;
        }
        JSONP.prototype.send = function () {
            function uuid() {
                function s4() {
                    return Math.floor((1 + Math.random()) * 0x10000)
                        .toString(16)
                        .substring(1);
                }
                return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
                    s4() + '-' + s4() + s4() + s4();
            }
            var id = uuid();
            var url = this.url;
            var callback_name = "callback";
            var timeout = 10;
            if (url.indexOf('?') === -1)
                url += "?callback={@callback}";
            else
                url += "&callback={@callback}";
            url = url.replace("{@callback}", encodeURIComponent(callback_name));
            if (this.queryData != null) {
                if (!(typeof this.queryData == 'string' || this.queryData instanceof String)) {
                    var query = [];
                    for (var i = 0, keys = Object.keys(this.queryData); i < keys.length; i++) {
                        query.push(encodeURIComponent(keys[i]) + '=' + encodeURIComponent(this.queryData[keys[i]]));
                    }
                    url += "&" + query.join('&');
                }
                else {
                    var qs = this.queryData;
                    if (qs.substr(0, 1) === "?" || qs.substr(0, 1) === "&") {
                        qs = qs.substr(1);
                    }
                    url += "&" + qs;
                }
            }
            function removeElement(id) {
                var el = document.getElementById(id);
                if (el != null)
                    el.parentElement.removeChild(el);
            }
            var timeout_trigger = window.setTimeout(function () {
                window[callback_name] = function () { };
                console.log("JSONP: script.ontimeout", arguments);
                this.cancelCallback(arguments);
                this.alwaysCallback(arguments);
                removeElement(id);
            }.bind(this), timeout * 1000);
            window[callback_name] = function (data) {
                window.clearTimeout(timeout_trigger);
                this.successCallback(data);
                this.alwaysCallback(data);
                removeElement(id);
            }.bind(this);
            var script = document.createElement('script');
            script.id = id;
            script.type = 'application/javascript';
            script.async = true;
            script.src = url;
            script.onerror = function (that, ev) {
                window.clearTimeout(timeout_trigger);
                console.log("JSONP: script.onerror", arguments);
                this.failureCallback(arguments);
                this.alwaysCallback(arguments);
                removeElement(id);
            }.bind(this);
            document.getElementsByTagName('head')[0].appendChild(script);
            return this;
        };
        JSONP.prototype.sendAsync = function () {
            return new Promise(function (resolve, reject) {
                this.success(function (result) {
                    resolve(result);
                });
                this.failure(function () {
                    console.log("onError", arguments);
                    reject(arguments);
                });
                this.cancel(function () {
                    console.log("onCancel", arguments);
                    reject(arguments);
                });
                this.send();
            }.bind(this));
        };
        return JSONP;
    }(RequestBase));
    Http1.JSONP = JSONP;
})(Http1 || (Http1 = {}));
