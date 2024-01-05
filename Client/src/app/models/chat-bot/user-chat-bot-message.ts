export interface UserChatBotMessage {
    id: number
    content: string
    messagedOn: string | null
    isBotMessage: boolean
}
