
import { Roles } from "../account/roles.enum"
import { Doctor } from "./doctor"


export interface User {
    id: number
    firstName: string
    lastName: string
    email: string
    imageUrl: string
    joinedOn: string
    doctor: Doctor
    role: Roles
}
