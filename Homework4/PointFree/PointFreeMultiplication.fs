module PointFreeMultiplication

let multiplyBy = (*) >> List.map

// List.map (fun y -> y * x) l ->
// List.map ((*) x) l  ->
// List.map << (*)
