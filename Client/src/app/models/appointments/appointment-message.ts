import { Media } from "../media"
import { User } from "../users/user"

export interface AppointmentMessage {
    id: number
    content: string
    medias: Media[] | undefined
    messagedOn: string | null
    sender: User
}
