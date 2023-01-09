import { Component, OnInit } from '@angular/core';
import { OfferDto } from 'src/app/models/offerDto';

@Component({
  selector: 'app-teacher-view',
  templateUrl: './teacher-view.component.html',
  styleUrls: ['./teacher-view.component.scss']
})
export class TeacherViewComponent implements OnInit {
  selectedDate: Date | null = null;
  offers: OfferDto[] = [];

  ngOnInit(): void {
    // Test Data for Offers
    this.offers.push({
      title: 'HTL',
      currentCount: 12,
      minCount: 3,
      maxCount: 25,
      description: 'HTL Grieskirchen',
      enrolled: false,
      location: 'Parzer Schulstraße 1',
      meetingPoint: 'Aula',
      price: 4,
      startDates: ['2022-03-01'],
      endDates: ['2022-03-01'],
      teachers: ['Grüneis', 'Welsch'],
    });
  }
}
