import { OfferSimpleDto } from './../../swagger/model/offerSimpleDto';
import { Component, OnInit } from '@angular/core';
import { OffersService } from '../../swagger';

@Component({
  selector: 'app-courses-list',
  templateUrl: './courses-list.component.html',
  styleUrls: ['./courses-list.component.scss']
})
export class CoursesListComponent implements OnInit {

  allCourses: OfferSimpleDto[] = [];

  constructor(private offersService: OffersService) { }

  ngOnInit(): void {
    console.log('admin::courses-list - Courses List works');
    this.offersService.offersGetOffersGet()
      .subscribe((x: OfferSimpleDto[]) => {
        this.allCourses = x;
        console.table(x);
      });
  }

  deleteCourse(offerId: number): void {
    console.log(`admin::courses-list::deleteCourse - Button Delete clicked - offerId:${offerId}`);
    this.offersService.offersDeleteOfferDelete(offerId)
      .subscribe((x: boolean) => {
        console.table(x);
        this.reloadField();
      });
  }

  editCourse(offerId: number): void {
    console.log(`admin::courses-list::editCourse - Button Edit clicked - offerId:${offerId}`);
    this.reloadField();
  }

  reloadField(): void {
    this.offersService.offersGetOffersGet()
      .subscribe((x: OfferSimpleDto[]) => {
        this.allCourses = x;
        console.table(x);
      });
  }
}
