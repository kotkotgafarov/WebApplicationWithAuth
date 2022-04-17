export class Competition {
  constructor(
    public id?: number,
    public code?: string,
    public speciality?: string,
    public department?: string,
    public budget?: string,
    public form?: string,
    public description?: string,
    public selected?: boolean
  ) { }
}
