export interface AuthResult {
    id: number
    name: string
    email: string
    role: string
<<<<<<< HEAD
=======
    imageUrl: string | null
>>>>>>> f89b181cb3a284fefa679d8b2d4d8c350d335dcf
    jwtToken: string
    jwtTokenExpiresOn: Date
    refreshToken: string
    refreshTokenExpiresON: Date
}