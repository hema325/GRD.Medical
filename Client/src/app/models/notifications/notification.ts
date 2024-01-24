import { User } from "../users/user"

export interface Notification {
    id: number
    content: string
    isRead: boolean
    notifiedOn: string
    referenceId: number
    referenceType: string
    initiator: User
}
