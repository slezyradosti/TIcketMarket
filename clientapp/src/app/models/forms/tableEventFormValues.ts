export class TableEventFormValues {
    id: string | null = null;
    eventId: string = '';
    tableId: string = '';

    constructor(tableEvent?: TableEventFormValues) {
        if (tableEvent) {
            this.eventId = tableEvent.eventId
            this.tableId = tableEvent.tableId
            this.id = tableEvent.id
        }
    }
}