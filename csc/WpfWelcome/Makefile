# for Cygwin

NAME    := WpfWelcome
TRGT    := $(NAME).exe
SRCDIR  := src
REQUIRE := $(SRCDIR)/$(NAME).cs
SRCFILE := $(SRCDIR)\$(NAME).cs
OUTDIR  := bin
OUTFILE := $(OUTDIR)/$(TRGT)

CSCPATH := C:\Windows\Microsoft.NET\Framework\v4.0.30319
CSC     := $(CSCPATH)\csc.exe
WPFPATH := $(CSCPATH)\WPF
DLL0    := /reference:$(WPFPATH)\PresentationCore.dll
DLL1    := /reference:$(WPFPATH)\PresentationFramework.dll
DLL2    := /reference:$(WPFPATH)\WindowsBase.dll
DLL3    := /reference:$(CSCPATH)\System.Xaml.dll
CSC_OPT := /utf8output /platform:anycpu /target:winexe $(DLL0) $(DLL1) $(DLL2) $(DLL3)

$(OUTFILE): $(REQUIRE)
	$(CSC) $(CSC_OPT) $(SRCFILE)
	mkdir -p $(OUTDIR)
	mv $(TRGT) $(OUTFILE)
