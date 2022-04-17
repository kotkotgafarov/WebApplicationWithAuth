export class ApplicationsExam {
  constructor(
    public id?: number,
    public competitionsExamId?: number,
    public enrolleeId?: number,
    public type?: number,
    public subjectId?: number,
    public subjectsSubstitutorId?: number,
    public score?: number,
    public enrolleesDocId?: number,
    public name?: string,
    public subjectName?: string,
    public subjectsSubstitutorName?: string,
    public egeIsFeasible?: boolean,
    public examIsFeasible?: boolean,
    public achievementIsFeasible?: boolean,
    public minScore?: number,
    public maxScore?: number,
    public enrolleesDocName?: string
  ) { }
}
