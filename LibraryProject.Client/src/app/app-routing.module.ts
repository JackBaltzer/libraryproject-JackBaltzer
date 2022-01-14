import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthorComponent } from './admin/author/author.component';
import { FrontpageComponent } from './frontpage/frontpage.component';

const routes: Routes = [
  { path: '', component: FrontpageComponent },
  { path: 'admin/author', component: AuthorComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
