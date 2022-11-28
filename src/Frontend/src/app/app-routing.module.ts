import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', loadChildren: () => import('./login/login.module').then(x => x.LoginModule) },
  { path: 'student', loadChildren: () => import('./student/student.module').then(x => x.StudentModule) },
  { path: 'teacher', loadChildren: () => import('./teacher/teacher.module').then(x => x.TeacherModule) },
  { path: 'admin', loadChildren: () => import('./admin/admin.module').then(x => x.AdminModule) }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
