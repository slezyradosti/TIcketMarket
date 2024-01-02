import { BaseModel } from "../BaseModel";
import { User } from "./user";

export interface TicketDiscount extends BaseModel {
    discountPercentage: number;
    code: string;
    isActivated: boolean;
    user: User | null;
    userId: string;
}