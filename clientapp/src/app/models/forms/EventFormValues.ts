export class EventFormValues {
    title: string = '';
    categoryId: string = '';
    description: string = '';
    place: string = '';
    date: Date = new Date((new Date()).getTime());
    typeId: string = '';
    moderator: string = '';
    totalPlaces: number = 0;
    id: string | null = null;

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
            this.id = event.id
        }
    }

}