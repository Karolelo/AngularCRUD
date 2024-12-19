import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable,BehaviorSubject,tap } from 'rxjs';
import {Category} from '../../../Intefraces/category';
@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  private baseUrl = 'https://localhost:7200/Category';
  private categoriesSubject = new BehaviorSubject<Category[]>([]);
  categories$ = this.categoriesSubject.asObservable();
  constructor(private http:HttpClient) { }

  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(`${this.baseUrl}/all`).pipe(
      tap((categories) => this.categoriesSubject.next(categories))
    );
  }

  deleteCategory(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(
      tap(() => {
        const updatedCategories = this.categoriesSubject.value.filter((category) => category.id !== id);
        this.categoriesSubject.next(updatedCategories);
      })
    );
  }

  addCategory(category: any): Observable<Category> {
    return this.http.post<Category>(this.baseUrl, category).pipe(
      tap((newCategory) => {
        const updatedCategories = [...this.categoriesSubject.value, newCategory];
        this.categoriesSubject.next(updatedCategories);
      })
    );
  }
}
