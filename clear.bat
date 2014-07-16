for /d /r "." %%d in (bin, obj, FakesAssemblies, Fakes, TestResults ) do @if exist "%%d" rd /s/q "%%d"
