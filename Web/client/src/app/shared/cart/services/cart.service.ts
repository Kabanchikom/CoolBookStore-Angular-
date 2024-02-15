import { Injectable } from '@angular/core';
import { CartHttpService } from './http/cart-http-service';
import { Observable, Subject, switchMap, tap, of, BehaviorSubject, delay } from 'rxjs';
import ICartLine from 'src/app/ordering/cart/types/ICartLine';
import { ICartAddRequest, ICartUpdateRequest } from './http/types/cart';
import { AccountService } from '../../account/services/account.service';
import { IUser } from '../../account/types/IUser';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  public cart = new BehaviorSubject<ICartLine[]>([]);
  public countSubject = new Subject<number>();

  constructor(
    private http: CartHttpService,
    private accountService: AccountService
  ) { }

  getCart(): Observable<ICartLine[]> {
    return this.http.getCart().pipe(
      tap((response: ICartLine[]) => {
        this.cart.next(response);
      }) 
    );
  }

  getCount(): Observable<number> {
    return this.http.getCount().pipe(
      tap(
        count => this.countSubject.next(count)
      )
    );
  }

  add(cartLine: {
    name: string,
    imgSrc: string,
    oldPrice: number,
    newPrice?: number,
    description?: string,
    productId: string,
    quantity: number
  }): Observable<string> {
    return this.accountService.getUser().pipe(
      switchMap(
        (user: IUser) => {
          return this.http.add({...cartLine, ownerId: user.id}).pipe(
            tap((id: string) => {
              const cart = [...this.cart.getValue()];
              const existingItem = cart.find(x => x.productId === cartLine.productId);

              if (existingItem) {
                existingItem.quantity++;
              } else {
                cart.push({...cartLine, id: id, ownerId: user.id });
              }
              
              this.cart.next(cart);

              const initialValue = 0;
              const newCount = this.cart.getValue().reduce((quantity, x) => quantity + x.quantity, initialValue);

              this.countSubject.next(newCount);
            }))
        }
      )
    )
  }

  addRange(request: ICartAddRequest[]) {
    return this.http.addRange(request);
  }

  deleteLine(productId: string) {
    return this.http.deleteLine(productId).pipe(
      tap(() => {
        const newCart = this.cart.getValue().filter(x => x.productId !== productId);
        this.cart.next(newCart);
      })
    );
  }

  update(id: string, request: ICartUpdateRequest) {
    return this.http.update(id, request).pipe(tap(
      () => {
        const newCart = [...this.cart.getValue()];
        let newItem = newCart.find(x => x.id === id);
    
        if (newItem) {
          newCart[newCart.indexOf(newItem)] = {...newItem, ...request};
        }
        
        this.cart.next(newCart);
      }
    ));
  }

  clear() {
    return this.http.clear();
  }
}
