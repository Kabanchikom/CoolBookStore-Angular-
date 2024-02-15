import { HttpClient } from "@angular/common/http";
import { Directive } from "@angular/core";
import { environment } from "src/environments/environment.development";
import { AccountService } from "../account/services/account.service";

@Directive()
abstract class HttpBase {
    apiUrl = environment.apiUrl;

    constructor(
        protected http: HttpClient,
      ) {}
}

export default HttpBase;