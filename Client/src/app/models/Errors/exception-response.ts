import { ErrorResponse } from "./error-response";

export interface ExceptionResponse extends ErrorResponse {
    details: string
}
