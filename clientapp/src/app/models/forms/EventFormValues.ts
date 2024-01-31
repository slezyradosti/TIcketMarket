import { EventCategory } from "../catalogues/eventCategory";
import { EventType } from "../catalogues/eventType";
import { User } from "../tables/user";

export class EventFormValues {
    title: string = '';
    categoryId: string = '';
    description: string = '';
    place: string = '';
    date: Date | null = new Date((new Date()).getTime());
    typeId: string = '';
    moderator: string = '';
    totalPlaces: number = 0;

    constructor(event?: EventFormValues) {
        if (event) {
            this.title = event.title
            this.categoryId = event.categoryId
            this.description = event.description
            this.place = event.place
            this.date = event.date
            this.typeId = event.typeId
            this.moderator = event.moderator
            this.totalPlaces = event.totalPlaces
        }
    }

}