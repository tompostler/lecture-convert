#region Mono.Options suppressions
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Scope = "member", Target = "Mono.Options.StringCoda.#GetNextWidth(System.Collections.Generic.IEnumerator`1<System.Int32>,System.Int32,System.Nullable`1<System.Boolean>&)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Target = "Mono.Options.OptionValueCollection.#System.Collections.ICollection.IsSynchronized")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Target = "Mono.Options.OptionValueCollection.#System.Collections.ICollection.SyncRoot")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes", Scope = "member", Target = "Mono.Options.OptionValueCollection.#System.Collections.IList.IsFixedSize")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "OptionContext", Scope = "member", Target = "Mono.Options.OptionValueCollection.#AssertValid(System.Int32)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Scope = "member", Target = "Mono.Options.OptionValueCollection.#ToList()")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Option", Scope = "type", Target = "Mono.Options.Option")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Scope = "member", Target = "Mono.Options.Option.#.ctor(System.String,System.String,System.Int32,System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Scope = "member", Target = "Mono.Options.Option.#.ctor(System.String,System.String,System.Int32,System.Boolean)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "c", Scope = "member", Target = "Mono.Options.Option.#Parse`1(System.String,Mono.Options.OptionContext)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1", Scope = "member", Target = "Mono.Options.Option.#Parse`1(System.String,Mono.Options.OptionContext)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "type", Scope = "member", Target = "Mono.Options.Option.#ParsePrototype()")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Scope = "member", Target = "Mono.Options.Option.#ParsePrototype()")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Scope = "member", Target = "Mono.Options.Option.#AddSeparators(System.String,System.Int32,System.Collections.Generic.ICollection`1<System.String>)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "c", Scope = "member", Target = "Mono.Options.Option.#Invoke(Mono.Options.OptionContext)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "Mono.Options.Option.#Invoke(Mono.Options.OptionContext)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "c", Scope = "member", Target = "Mono.Options.Option.#OnParseComplete(Mono.Options.OptionContext)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Scope = "member", Target = "Mono.Options.ArgumentSource.#GetArguments(System.String,System.Collections.Generic.IEnumerable`1<System.String>&)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "Mono.Options.ArgumentSource.#GetArgumentsFromFile(System.String)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Scope = "type", Target = "Mono.Options.OptionException")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2135:SecurityRuleSetLevel2MethodsShouldNotBeProtectedWithLinkDemandsFxCopRule", Scope = "member", Target = "Mono.Options.OptionException.#GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Scope = "type", Target = "Mono.Options.OptionSet")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Scope = "member", Target = "Mono.Options.OptionSet.#GetKeyForItem(Mono.Options.Option)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "OnParseComplete", Scope = "member", Target = "Mono.Options.OptionSet+Category.#OnParseComplete(Mono.Options.OptionContext)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member", Target = "Mono.Options.OptionSet+ActionOption.#.ctor(System.String,System.String,System.Int32,System.Action`1<Mono.Options.OptionValueCollection>)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "Mono.Options.OptionSet+ActionOption.#OnParseComplete(Mono.Options.OptionContext)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "Mono.Options.OptionSet+ActionOption`1.#OnParseComplete(Mono.Options.OptionContext)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "Mono.Options.OptionSet+ActionOption`2.#OnParseComplete(Mono.Options.OptionContext)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Scope = "member", Target = "Mono.Options.OptionSet.#Parse(System.Collections.Generic.IEnumerable`1<System.String>)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#", Scope = "member", Target = "Mono.Options.OptionSet.#GetOptionParts(System.String,System.String&,System.String&,System.String&,System.String&)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "2#", Scope = "member", Target = "Mono.Options.OptionSet.#GetOptionParts(System.String,System.String&,System.String&,System.String&,System.String&)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "3#", Scope = "member", Target = "Mono.Options.OptionSet.#GetOptionParts(System.String,System.String&,System.String&,System.String&,System.String&)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "4#", Scope = "member", Target = "Mono.Options.OptionSet.#GetOptionParts(System.String,System.String&,System.String&,System.String&,System.String&)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "flag", Scope = "member", Target = "Mono.Options.OptionSet.#GetOptionParts(System.String,System.String&,System.String&,System.String&,System.String&)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "c", Scope = "member", Target = "Mono.Options.OptionSet.#Parse(System.String,Mono.Options.OptionContext)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1", Scope = "member", Target = "Mono.Options.OptionSet.#Parse(System.String,Mono.Options.OptionContext)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "OptionValueType", Scope = "member", Target = "Mono.Options.OptionSet.#ParseBundledValue(System.String,System.String,Mono.Options.OptionContext)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "o", Scope = "member", Target = "Mono.Options.OptionSet.#WriteOptionDescriptions(System.IO.TextWriter)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "Mono.Options.OptionSet.#WriteOptionDescriptions(System.IO.TextWriter)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces", Scope = "type", Target = "UnlimitedInf.LectureConvert.Options")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields", Scope = "member", Target = "UnlimitedInf.LectureConvert.Options.#LinkFile")]
#endregion

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Inf", Scope = "namespace", Target = "UnlimitedInf.LectureConvert")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "lectureconvert")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "lectureconvert")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "UnlimitedInf.LectureConvert.Convert.#SetUpProcesses(System.Collections.Generic.List`1<UnlimitedInf.LectureConvert.LectureInfo>)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA2210:AssembliesShouldHaveValidStrongNames")]

// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.
//
// To add a suppression to this file, right-click the message in the 
// Code Analysis results, point to "Suppress Message", and click 
// "In Suppression File".
// You do not need to add suppressions to this file manually.

