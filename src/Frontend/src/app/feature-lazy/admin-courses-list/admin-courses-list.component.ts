import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-admin-courses-list',
  templateUrl: './admin-courses-list.component.html',
  styleUrls: ['./admin-courses-list.component.scss']
})
export class AdminCoursesListComponent implements OnInit {

  allCourses: CourseTestClass[] = [];

  ngOnInit(): void {
    console.log('feature-lazy::admin-courses-list - Courses List works');
    this.allCourses.push({id:1, title:'Basketball',courseDateBegin:'01.06.2023',courseDateEnd:'02.06.2023',teacher:'Grüneis'});
    this.allCourses.push({id:2, title:'Klettern',courseDateBegin:'01.06.2023',courseDateEnd:'01.06.2023',teacher:'Mairinger'});
    this.allCourses.push({id:3, title:'Schach',courseDateBegin:'03.06.2023',courseDateEnd:'03.06.2023',teacher:'Grüneis'});
    this.allCourses.push({id:4, title:'Paintball',courseDateBegin:'02.06.2023',courseDateEnd:'03.06.2023',teacher:'Welsch'});    
  }

  deleteCourse(id: number): void {
    console.log(`feature-lazy::admin-courses-list::deleteCourse - Button Delete clicked - id:${id}`);
  }

  editCourse(id: number): void {
    console.log(`feature-lazy::admin-courses-list::editCourse - Button Edit clicked - id:${id}`);
  }
  
  dateConverter(dateBegin: string , dateEnd: string): string{
    if(dateBegin!==dateEnd){
      return `${dateBegin} - ${dateEnd}`;
    }
    return `${dateBegin}`;
  }
}
export interface OfferDto{
  offerId: number,
  title: string,
  // offerDates: OfferDate[],
  teacherId: number,
  // teacher: Teacher,
}
export interface CourseTestClass{
  id: number,
  title: string,
  courseDateBegin: string,
  courseDateEnd: string,
  teacher: string,
}
