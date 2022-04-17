import { Enrollee } from '../models/enrollee';
import { Relative } from '../models/relative';
import { EnrolleesDoc } from '../models/enrolleesdoc';
import { DocType } from '../models/doctype';
import { Competition } from './competition';
import { EnrolleesCompetition } from '../models/enrolleescompetition';
import { ApplicationsExam } from './applicationsexam';
import { SubjectsSubstitutor } from './subjectssubstitutor';
import { ApplicationsContract } from './applicationscontract';
import { ApplicationsAchievement } from './applicationsachievement';
import { EnrolleesStatus } from './enrolleesstatus';

export class Application {
  constructor(
    public enrollee: Enrollee,
    public competitions: Competition[],
    public applicationsexam: ApplicationsExam[],
    public subjectsubstitutors: SubjectsSubstitutor[],
    public docs: EnrolleesDoc[],
    public applicationscontract: ApplicationsContract[],
    public applicationsachievement: ApplicationsAchievement[],
    public enrolleesstatus: EnrolleesStatus
  ) { }
}
