export class EnrolleesDoc {
  constructor(
    public id?: number,
    public type?: string,
    public series?: string,
    public number?: string,
    public issueDate?: Date,
    public issuedBy?: string,
    public authority?: string,
    public enroleeId?: number,
    public changed?: boolean,
    public fileId?: number,
    public fileName?: string,
    public canBeDeleted?: boolean,
    public docTypeId?: number
  ) { }
}
