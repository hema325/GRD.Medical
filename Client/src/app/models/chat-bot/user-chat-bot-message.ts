export interface UserChatBotMessage {
    id: number
    content: string
    messagedOn: Date | null
    isBotMessage: boolean
}
