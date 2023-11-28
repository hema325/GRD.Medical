export interface AuthResult {
    id: number
    name: string
    email: string
    role: string
    jwtToken: string
    jwtTokenExpiresOn: Date
    refreshToken: string
    refreshTokenExpiresON: Date
}