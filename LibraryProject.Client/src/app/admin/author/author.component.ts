import { Component, OnInit } from '@angular/core';
import { Author } from 'src/app/_models/author';
import { AuthorService } from 'src/app/_services/author.service';

@Component({
  selector: 'app-author',
  templateUrl: './author.component.html',
  styleUrls: ['./author.component.css']
})
export class AuthorComponent implements OnInit {

  authors: Author[] = [];
  author: Author = { id: 0, firstName: '', lastName: '', birthYear: 0 };

  constructor(private authorService: AuthorService) { }

  ngOnInit(): void {
    this.authorService.getAllAuthors().subscribe(x => this.authors = x);
  }

  edit(author: Author): void {
    this.author = author;
  }

  delete(author: Author): void {
    if (confirm('Er du sikker på du vil slette?')) {
      this.authorService.deleteAuthor(author.id)
        .subscribe(() => {
          this.authors = this.authors.filter(x => x.id != author.id);
        });
    }
  }

  save(): void {
    if (this.author?.yearOfDeath == 0 || this.author?.yearOfDeath?.toString() == '') {
      delete this.author.yearOfDeath;
    }
    if (this.author.id == 0) {
      this.authorService.addAuthor(this.author)
        .subscribe({
          next: (x) => {
            this.authors.push(x);
            this.author = { id: 0, firstName: '', lastName: '', birthYear: 0 };
          },
          error: (err) => {
            console.log(err.error.errors.join(", "));
          }
        });
    } else {
      this.authorService.updateAuthor(this.author.id, this.author)
        .subscribe({
          error: (err) => {
            console.log(err.error.errors.join(", "));
          },
          complete: () => {
            this.author = { id: 0, firstName: '', lastName: '', birthYear: 0 };
          }
        });
    }
  }

  cancel(): void {
    this.author = { id: 0, firstName: '', lastName: '', birthYear: 0 };
  }
}
