##
## Auto Generated makefile by CodeLite IDE
## any manual changes will be erased      
##
## Debug
ProjectName            :=TD6_7
ConfigurationName      :=Debug
WorkspacePath          :=C:/Users/Nicolas/Documents/CodeLite/LangageC
ProjectPath            :=C:/Users/Nicolas/Documents/CodeLite/LangageC/td6-7-c
IntermediateDirectory  :=./Debug
OutDir                 := $(IntermediateDirectory)
CurrentFileName        :=
CurrentFilePath        :=
CurrentFileFullPath    :=
User                   :=Nicolas
Date                   :=20/12/2017
CodeLitePath           :="C:/Program Files/CodeLite"
LinkerName             :=C:/MinGW/bin/g++.exe
SharedObjectLinkerName :=C:/MinGW/bin/g++.exe -shared -fPIC
ObjectSuffix           :=.o
DependSuffix           :=.o.d
PreprocessSuffix       :=.i
DebugSwitch            :=-g 
IncludeSwitch          :=-I
LibrarySwitch          :=-l
OutputSwitch           :=-o 
LibraryPathSwitch      :=-L
PreprocessorSwitch     :=-D
SourceSwitch           :=-c 
OutputFile             :=$(IntermediateDirectory)/$(ProjectName)
Preprocessors          :=
ObjectSwitch           :=-o 
ArchiveOutputSwitch    := 
PreprocessOnlySwitch   :=-E
ObjectsFileList        :="TD6_7.txt"
PCHCompileFlags        :=
MakeDirCommand         :=makedir
RcCmpOptions           := 
RcCompilerName         :=C:/MinGW/bin/windres.exe
LinkOptions            :=  
IncludePath            :=  $(IncludeSwitch). $(IncludeSwitch). 
IncludePCH             := 
RcIncludePath          := 
Libs                   := 
ArLibs                 :=  
LibPath                := $(LibraryPathSwitch). 

##
## Common variables
## AR, CXX, CC, AS, CXXFLAGS and CFLAGS can be overriden using an environment variables
##
AR       := C:/MinGW/bin/ar.exe rcu
CXX      := C:/MinGW/bin/g++.exe
CC       := C:/MinGW/bin/gcc.exe
CXXFLAGS :=  -g -O0 -Wall $(Preprocessors)
CFLAGS   :=  -g -O0 -Wall $(Preprocessors)
ASFLAGS  := 
AS       := C:/MinGW/bin/as.exe


##
## User defined environment variables
##
CodeLiteDir:=C:\Program Files\CodeLite
Objects0=$(IntermediateDirectory)/main.c$(ObjectSuffix) $(IntermediateDirectory)/table.c$(ObjectSuffix) $(IntermediateDirectory)/fonctionnaire.c$(ObjectSuffix) $(IntermediateDirectory)/vecteur.c$(ObjectSuffix) 



Objects=$(Objects0) 

##
## Main Build Targets 
##
.PHONY: all clean PreBuild PrePreBuild PostBuild MakeIntermediateDirs
all: $(OutputFile)

$(OutputFile): $(IntermediateDirectory)/.d $(Objects) 
	@$(MakeDirCommand) $(@D)
	@echo "" > $(IntermediateDirectory)/.d
	@echo $(Objects0)  > $(ObjectsFileList)
	$(LinkerName) $(OutputSwitch)$(OutputFile) @$(ObjectsFileList) $(LibPath) $(Libs) $(LinkOptions)

MakeIntermediateDirs:
	@$(MakeDirCommand) "./Debug"


$(IntermediateDirectory)/.d:
	@$(MakeDirCommand) "./Debug"

PreBuild:


##
## Objects
##
$(IntermediateDirectory)/main.c$(ObjectSuffix): main.c $(IntermediateDirectory)/main.c$(DependSuffix)
	$(CC) $(SourceSwitch) "C:/Users/Nicolas/Documents/CodeLite/LangageC/td6-7-c/main.c" $(CFLAGS) $(ObjectSwitch)$(IntermediateDirectory)/main.c$(ObjectSuffix) $(IncludePath)
$(IntermediateDirectory)/main.c$(DependSuffix): main.c
	@$(CC) $(CFLAGS) $(IncludePath) -MG -MP -MT$(IntermediateDirectory)/main.c$(ObjectSuffix) -MF$(IntermediateDirectory)/main.c$(DependSuffix) -MM main.c

$(IntermediateDirectory)/main.c$(PreprocessSuffix): main.c
	$(CC) $(CFLAGS) $(IncludePath) $(PreprocessOnlySwitch) $(OutputSwitch) $(IntermediateDirectory)/main.c$(PreprocessSuffix) main.c

$(IntermediateDirectory)/table.c$(ObjectSuffix): table.c $(IntermediateDirectory)/table.c$(DependSuffix)
	$(CC) $(SourceSwitch) "C:/Users/Nicolas/Documents/CodeLite/LangageC/td6-7-c/table.c" $(CFLAGS) $(ObjectSwitch)$(IntermediateDirectory)/table.c$(ObjectSuffix) $(IncludePath)
$(IntermediateDirectory)/table.c$(DependSuffix): table.c
	@$(CC) $(CFLAGS) $(IncludePath) -MG -MP -MT$(IntermediateDirectory)/table.c$(ObjectSuffix) -MF$(IntermediateDirectory)/table.c$(DependSuffix) -MM table.c

$(IntermediateDirectory)/table.c$(PreprocessSuffix): table.c
	$(CC) $(CFLAGS) $(IncludePath) $(PreprocessOnlySwitch) $(OutputSwitch) $(IntermediateDirectory)/table.c$(PreprocessSuffix) table.c

$(IntermediateDirectory)/fonctionnaire.c$(ObjectSuffix): fonctionnaire.c $(IntermediateDirectory)/fonctionnaire.c$(DependSuffix)
	$(CC) $(SourceSwitch) "C:/Users/Nicolas/Documents/CodeLite/LangageC/td6-7-c/fonctionnaire.c" $(CFLAGS) $(ObjectSwitch)$(IntermediateDirectory)/fonctionnaire.c$(ObjectSuffix) $(IncludePath)
$(IntermediateDirectory)/fonctionnaire.c$(DependSuffix): fonctionnaire.c
	@$(CC) $(CFLAGS) $(IncludePath) -MG -MP -MT$(IntermediateDirectory)/fonctionnaire.c$(ObjectSuffix) -MF$(IntermediateDirectory)/fonctionnaire.c$(DependSuffix) -MM fonctionnaire.c

$(IntermediateDirectory)/fonctionnaire.c$(PreprocessSuffix): fonctionnaire.c
	$(CC) $(CFLAGS) $(IncludePath) $(PreprocessOnlySwitch) $(OutputSwitch) $(IntermediateDirectory)/fonctionnaire.c$(PreprocessSuffix) fonctionnaire.c

$(IntermediateDirectory)/vecteur.c$(ObjectSuffix): vecteur.c $(IntermediateDirectory)/vecteur.c$(DependSuffix)
	$(CC) $(SourceSwitch) "C:/Users/Nicolas/Documents/CodeLite/LangageC/td6-7-c/vecteur.c" $(CFLAGS) $(ObjectSwitch)$(IntermediateDirectory)/vecteur.c$(ObjectSuffix) $(IncludePath)
$(IntermediateDirectory)/vecteur.c$(DependSuffix): vecteur.c
	@$(CC) $(CFLAGS) $(IncludePath) -MG -MP -MT$(IntermediateDirectory)/vecteur.c$(ObjectSuffix) -MF$(IntermediateDirectory)/vecteur.c$(DependSuffix) -MM vecteur.c

$(IntermediateDirectory)/vecteur.c$(PreprocessSuffix): vecteur.c
	$(CC) $(CFLAGS) $(IncludePath) $(PreprocessOnlySwitch) $(OutputSwitch) $(IntermediateDirectory)/vecteur.c$(PreprocessSuffix) vecteur.c


-include $(IntermediateDirectory)/*$(DependSuffix)
##
## Clean
##
clean:
	$(RM) -r ./Debug/


