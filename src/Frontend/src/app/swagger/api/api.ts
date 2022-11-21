export * from './authentication.service';
import { AuthenticationService } from './authentication.service';
export * from './offers.service';
import { OffersService } from './offers.service';
export * from './students.service';
import { StudentsService } from './students.service';
export * from './testMail.service';
import { TestMailService } from './testMail.service';
export const APIS = [AuthenticationService, OffersService, StudentsService, TestMailService];
