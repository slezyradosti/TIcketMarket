import { BaseModel } from "../BaseModel";
import { EventCategory } from "../catalogues/eventCategory";
import { EventType } from "../catalogues/eventType";
import { User } from "./user";

export interface Event extends BaseModel {
    title: string;
    category: EventCategory | null;
    categoryId: string;
    description: string;
    place: string;
    date: Date | undefined;
    user: User | null;
    userId: string;
    type: EventType | null;
    typeId: string;
    moderator: string;
    totalPlaces: number;
}