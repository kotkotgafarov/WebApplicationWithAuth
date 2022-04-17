export class ApplicationsContract {
  constructor(
    public id?: number,
    public enrolleeId?: number,
    public enrolleesDocId?: number,
    public client?: string,
    public enrolleesDocName?: string
  ) { }
}
