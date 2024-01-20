import { ErrorCodes } from "./error-codes.enum";

export interface ErrorResponse {
    errorCode: ErrorCodes
    statusCode: number
    message: string
}
