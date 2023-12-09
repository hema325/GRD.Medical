export interface AuthResult {
    id: number
    name: string
    email: string
    role: string
    imageUrl: string | null
    jwtToken: string
    jwtTokenExpiresOn: Date
    refreshToken: string
    refreshTokenExpiresON: Date
}