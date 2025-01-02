import { Component,OnInit,Input } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import {Category} from '../../../../Intefraces/category';
import {CategoryService} from '../../CategoryService/category.service';

@Component({
  selector: 'app-category-list',
  standalone: false,

  templateUrl: './category-list.component.html',
  styleUrl: './category-list.component.css'
})
export class CategoryListComponent implements OnInit {
  private categoriesSubject = new BehaviorSubject<Category[]>([]);

  categories$ = this.categoriesSubject.asObservable();

  constructor(private categoryService: CategoryService) {}

  ngOnInit(): void {
    this.categoryService.categories$.subscribe((categories) => {
      this.categoriesSubject.next(categories);
    })
  }

  removeCategory(id: number): void {
    console.log("produkt usuniety "+id);
    this.categoryService.deleteCategory(id).subscribe(() => {
      const currentCategories = this.categoriesSubject.value.filter((category) => category.id !== id);
      this.categoriesSubject.next(currentCategories);
    });
  }
}
