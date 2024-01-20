import { ErrorResponse } from "./error-response";

export interface ValidationResponse extends ErrorResponse {
    validationResponse: string[]
}
