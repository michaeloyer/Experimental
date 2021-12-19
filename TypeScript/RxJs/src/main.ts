import "./style.css";
import {
    distinctUntilChanged,
    fromEvent,
    map,
    of,
    ReplaySubject,
    Subject,
} from "rxjs";
// const app = document.querySelector<HTMLDivElement>("#app");
type Person = { id: number; first: string; last: string };
let people = [
    { id: 1, first: "John", last: "Doe" },
    { id: 2, first: "Jane", last: "Smith" },
];

let peopleSubject = new Subject<Person[]>();
let people$ = peopleSubject.asObservable();

function comp<T>(p: T, c: T) {
    return JSON.stringify(p) === JSON.stringify(c);
}

people$
    .pipe(
        map((x) => x.find((person) => person.id === 2)),
        distinctUntilChanged(comp)
    )
    .subscribe((person) =>
        console.log("The person is: " + JSON.stringify(person))
    );

console.log("next 1");
peopleSubject.next([
    { id: 1, first: "John", last: "Doe" },
    { id: 2, first: "Jane", last: "Smith" },
]);

console.log("next 2");
peopleSubject.next([
    { id: 1, first: "John", last: "Smith" },
    { id: 2, first: "Jane", last: "Smith" },
]);

console.log("next 3");
peopleSubject.next([
    { id: 1, first: "John", last: "Doe" },
    { id: 2, first: "Jane", last: "Doe" },
]);

let s = new ReplaySubject<number>();

let click$ = fromEvent(
    document.getElementById("Test") as HTMLButtonElement,
    "click"
);

let numbers$ = of(1, 2, 3, 4, 5);

let obs$ = s.asObservable().pipe(
    // shareReplay(1),
    map((i) => i + 1)
);

console.log("Test!");

obs$.subscribe((i) => console.log(i));

s.next(1);
s.next(2);
s.next(3);

obs$.subscribe((i) => console.log(i));

s.next(4);
s.next(5);
s.next(6);
