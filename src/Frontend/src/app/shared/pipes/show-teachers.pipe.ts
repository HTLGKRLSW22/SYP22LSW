import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'showTeachers'
})
export class ShowTeachersPipe implements PipeTransform {

  transform(teachers: string[]): string {
    let result = '& ';

    teachers.forEach(x => {
      if (teachers.indexOf(x) !== 0 && teachers.indexOf(x) === teachers.length - 1) {
        result += `${x}`;
      } else if (teachers.indexOf(x) !== 0) {
        result += `${x}, `;
      }
    });

    return result;
  }

}
