
export interface ServerError {
  statusCode: number;
  message: string;
  details: string;
}


// APi -> agent -> directly to component (not passing it to a parent component like we did with <ValidationErrors />)
//  API -> agent -> STORE -> component

