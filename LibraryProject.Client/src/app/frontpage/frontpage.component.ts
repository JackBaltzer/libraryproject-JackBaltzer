import { Component, OnInit } from '@angular/core';
import { Author } from '../_models/author';
import { AuthorService } from '../_services/author.service';

@Component({
  selector: 'app-frontpage',
  templateUrl: './frontpage.component.html',
  styleUrls: ['./frontpage.component.css']
})
export class FrontpageComponent implements OnInit {

  authors: Author[] = [];

  constructor(private authorService:AuthorService) { }

  ngOnInit(): void {
    // this.authors.push({ id: 1, firstName: "George", lastName: "Martin", middleName: "R.R.", birthYear: 1948 });
    // this.authors.push({ id: 2, firstName: "Lewis", lastName: "Carol", birthYear: 1832, yearOfDeath: 1898 });
    this.authorService.getAllAuthors()
      .subscribe(x => this.authors = x);
  }

}
