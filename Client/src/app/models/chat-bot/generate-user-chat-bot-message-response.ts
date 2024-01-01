import { UserChatBotMessage } from "./user-chat-bot-message"

export interface GenerateUserChatBotMessageResponse {
    userMessage: UserChatBotMessage
    chatBotMessage: UserChatBotMessage
    isFailedToGenerateResponse: boolean
}
