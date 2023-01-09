export interface OfferDto {
    title: string;
	description: string | null;
	teachers : string[];
	price : number | null;
	startDates : string[];
	endDates : string[];
    currentCount : number;
	maxCount : number;
	minCount : number;
	location : string | null;
	meetingPoint : string | null;
	enrolled : boolean | null;
}