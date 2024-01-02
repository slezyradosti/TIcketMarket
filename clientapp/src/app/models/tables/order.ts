import { BaseModel } from "../BaseModel";
import { User } from "./user";

export interface Order extends BaseModel {
    user: User | null;
    userId: string;
}