import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'showTimesForDates'
})
export class ShowTimesForDatesPipe implements PipeTransform {

  transform(startDateString: string, endDates: string[], days: string[]): string {
    let result = '';

    if (endDates.length !== 0) {
      const startDate: Date = new Date(startDateString);
      const endDate: Date = new Date(endDates.filter(x => new Date(x).toDateString() === startDate.toDateString())[0]);

      result = `${days[startDate.getDay() - 1]}: ${startDate.getHours() / 10 < 1 ? `0${startDate.getHours()}` : startDate.getHours()}:${startDate.getMinutes() / 10 < 1 ? `0${startDate.getMinutes()}` : startDate.getMinutes()} - ${endDate.getHours() / 10 < 1 ? `0${endDate.getHours()}` : endDate.getHours()}:${endDate.getMinutes() / 10 < 1 ? `0${endDate.getMinutes()}` : endDate.getMinutes()}`;
    }

    return result;
  }

}
