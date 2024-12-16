import {Category} from './category';

export interface Product {

  id: number;
  name: string;
  price: number;
  imgSourcePath: string;
  magazine: {
    productId: number;
    quanity:number;
  }
  categories: Category[];

}
