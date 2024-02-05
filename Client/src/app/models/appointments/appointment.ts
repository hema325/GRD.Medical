import { User } from "../users/user"

export interface Appointment {
    id: number
    status: string
    startDateTime: string
    endDateTime: string
    patient: User
    doctor: User
    consultFee: number
}
