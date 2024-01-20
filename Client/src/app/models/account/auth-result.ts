import { Roles } from "./roles.enum"

export interface AuthResult {
    id: number
    name: string
    email: string
    role: Roles
    imageUrl: string | null
    jwtToken: string
    jwtTokenExpiresOn: Date
    refreshToken: string
    refreshTokenExpiresOn: Date
}